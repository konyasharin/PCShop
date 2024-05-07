import Input from 'components/inputs/Input/Input.tsx';
import { useEffect, useState } from 'react';
import styles from './Orders.module.css';
import CheckBox from 'components/CheckBox/CheckBox.tsx';
import useCheckBoxes from 'hooks/useCheckBoxes.ts';
import OrderBlock from './OrderBlock/OrderBlock.tsx';
import ShowMoreBtn from 'components/btns/ShowMoreBtn/ShowMoreBtn.tsx';
import getOrders from 'api/orders/getOrders.ts';
import TOrder from 'types/orders/TOrder.ts';
import { useDispatch } from 'react-redux';
import { setIsLoading } from 'store/slices/loadingSlice.ts';

function Orders() {
  const [id, setId] = useState('');
  const { checkBoxes, setCheckBoxIsActive } = useCheckBoxes([
    { text: 'Принят', isActive: false },
    { text: 'В обработке', isActive: false },
    { text: 'В пути', isActive: false },
    { text: 'Доставлен', isActive: false },
  ]);
  const checkBoxesBlock = checkBoxes.map((checkBox, i) => {
    return (
      <CheckBox
        className={styles.filter}
        isActive={checkBox.isActive}
        text={checkBox.text}
        onChange={() => setCheckBoxIsActive(i, !checkBox.isActive)}
      />
    );
  });
  const [orders, setOrders] = useState<TOrder[]>([]);
  const dispatch = useDispatch();
  useEffect(() => {
    dispatch(setIsLoading(true));
    getOrders(3, 0).then(response => {
      setOrders(response.data.orders);
      dispatch(setIsLoading(false));
    });
  }, []);

  const ordersBlocks = orders.map(order => {
    return (
      <OrderBlock
        className={styles.order}
        orderNumber={order.orderId}
        status={order.orderStatus}
      />
    );
  });
  return (
    <div className={styles.orders}>
      <Input
        placeholder={'Поиск по id'}
        value={id}
        onChange={newValue => setId(newValue)}
        type={'number'}
      />
      <div className={styles.filters}>{...checkBoxesBlock}</div>
      {...ordersBlocks}
      <ShowMoreBtn>Показать еще...</ShowMoreBtn>
    </div>
  );
}

export default Orders;
