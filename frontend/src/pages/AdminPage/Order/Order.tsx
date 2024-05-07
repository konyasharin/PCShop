import { useParams } from 'react-router-dom';
import styles from './Order.module.css';
import createOrderStatusColor from '../createOrderStatusColor.ts';
import MainBtn from 'components/btns/MainBtn/MainBtn.tsx';
import OrderCard from 'components/cards/OrderCard/OrderCard.tsx';
import { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { setIsLoading } from 'store/slices/loadingSlice.ts';
import getOrderInfo from 'api/orders/getOrderInfo.ts';
import TProductInfo from 'types/TProductInfo.ts';
import getProduct from 'api/getProduct.ts';
import config from '../../../../config.ts';
import TOrder from 'types/orders/TOrder.ts';
import getOrder from 'api/orders/getOrder.ts';
import orderStatuses from 'enums/orderStatuses.ts';
import updateOrderStatus from 'api/orders/updateOrderStatus.ts';

function Order() {
  const { orderNumber } = useParams();
  const dispatch = useDispatch();
  const [products, setProducts] = useState<TProductInfo[]>([]);
  const [order, setOrder] = useState<TOrder | null>(null);
  function updateOrderStatusHandle(newOrderStatus: keyof typeof orderStatuses) {
    if (order) {
      dispatch(setIsLoading(true));
      updateOrderStatus(order.orderId, newOrderStatus).then(response => {
        setOrder({
          ...order,
          orderStatus: response.data.newStatus,
        });
        dispatch(setIsLoading(false));
      });
    }
  }
  function getNextOrderStatus(
    orderStatus: keyof typeof orderStatuses,
  ): keyof typeof orderStatuses {
    switch (orderStatus) {
      case 'accepted':
        return 'inProcessing';
      case 'inProcessing':
        return 'onTheWay';
      case 'onTheWay':
        return 'delivered';
      case 'delivered':
        return 'delivered';
    }
  }
  useEffect(() => {
    async function getOrderPageData(orderId: number) {
      dispatch(setIsLoading(true));
      setOrder((await getOrder(orderId)).data.order);
      getOrderInfo(orderId).then(response => {
        const promises = response.data.orderInfo.map(elem => {
          return getProduct(elem.productId);
        });
        Promise.all(promises).then(responses => {
          responses.forEach(onceResponse => {
            setProducts([...products, onceResponse.data.product]);
          });
        });
      });
    }
    if (orderNumber != undefined) {
      setProducts([]);
      setOrder(null);
      getOrderPageData(+orderNumber).then(() => {
        dispatch(setIsLoading(false));
      });
    }
  }, []);
  const orderCards = products.map(product => {
    return (
      <OrderCard
        url={`/product/${product.productType}/${product.productId}`}
        name={`${product.brand} ${product.model}`}
        text={product.description}
        img={`${config.backupUrl}/${product.image}`}
        price={product.price}
      />
    );
  });

  return (
    <div className={styles.order}>
      <div className={styles.orderInfo}>
        <div>Заказ #{orderNumber}</div>
        <div className={styles.status}>
          Статус:{' '}
          <div
            style={{
              color: order
                ? createOrderStatusColor(order.orderStatus)
                : 'black',
            }}
            className={styles.statusText}
          >
            {order ? orderStatuses[order.orderStatus] : ''}
          </div>
        </div>
        <MainBtn
          className={styles.btn}
          onClick={() => {
            if (order) {
              updateOrderStatusHandle(getNextOrderStatus(order.orderStatus));
            }
          }}
        >
          {order ? orderStatuses[getNextOrderStatus(order.orderStatus)] : ''}
        </MainBtn>
      </div>
      {...orderCards}
    </div>
  );
}

export default Order;
