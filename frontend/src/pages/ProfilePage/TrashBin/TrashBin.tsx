import ChooseBinCard from 'components/cards/ChooseBinCard/ChooseBinCard.tsx';
import styles from './TrashBin.module.css';
import Btn from 'components/btns/Btn/Btn.tsx';
import { ReactNode, useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { RootState } from 'store/store.ts';
import getCart from 'api/cart/getCart.ts';
import { setIsLoading } from 'store/slices/loadingSlice.ts';
import TProductInfo from 'types/TProductInfo.ts';
import getProduct from 'api/getProduct.ts';
import { AxiosResponse } from 'axios';
import config from '../../../../config.ts';
import deleteFromCart from 'api/cart/deleteFromCart.ts';
import { setCart, setUserData } from 'store/slices/profileSlice.ts';
import createOrder from 'api/orders/createOrder.ts';
import editUser from 'api/user/editUser.ts';
import balance from '../Balance/Balance.tsx';

function TrashBin() {
  const userInfo = useSelector((state: RootState) => state.profile.userInfo);
  const cart = useSelector((state: RootState) => state.profile.cart);
  const [cartProducts, setCartProducts] = useState<TProductInfo[] | null>([]);
  const dispatch = useDispatch();
  useEffect(() => {
    if (userInfo) {
      dispatch(setIsLoading(true));
      const promises: Promise<AxiosResponse<{ product: TProductInfo }>>[] = [];
      getCart(userInfo.id).then(response => {
        response.data.productsArray.forEach(product => {
          promises.push(getProduct(product.productId));
        });
        const newCartProducts: TProductInfo[] = [];
        Promise.all(promises).then(responses => {
          responses.forEach(response => {
            newCartProducts.push(response.data.product);
          });
          setCartProducts(newCartProducts);
          dispatch(setIsLoading(false));
        });
      });
    }
  }, [userInfo, cart]);

  const cartProductsBlocks: ReactNode[] = [];
  let sum: number = 0;
  if (cartProducts && userInfo) {
    cartProducts.forEach(product => {
      cartProductsBlocks.push(
        <ChooseBinCard
          className={styles.card}
          name={`${product.brand} ${product.model}`}
          img={`${config.backupUrl}/${product.image}`}
          text={product.description}
          price={product.price}
          url={`/product/${product.productType}/${product.productId}`}
          onDelete={() => {
            dispatch(setIsLoading(true));
            deleteFromCart({
              userId: userInfo.id,
              productId: product.productId,
            }).then(() => {
              dispatch(
                setCart(
                  cart.filter(
                    productFilter =>
                      productFilter.productId != product.productId,
                  ),
                ),
              );
              dispatch(setIsLoading(false));
            });
          }}
        />,
      );
      sum += product.price;
    });
  }

  return (
    <div className={styles.block}>
      {...cartProductsBlocks}
      <div className={styles.end}>
        <h4>Всего:</h4>
        <h2 className={styles.price}>{sum}$</h2>
      </div>
      <Btn
        className={styles.button}
        onClick={() => {
          if (cartProducts && userInfo && userInfo.balance >= sum) {
            dispatch(setIsLoading(true));
            createOrder(
              { orderStatus: 'accepted', userId: userInfo.id },
              cartProducts.map(product => {
                return {
                  productId: product.productId,
                  productType: product.productType,
                };
              }),
            ).then(() => {
              const promises: Promise<AxiosResponse<{ id: number }>>[] = [];
              cartProducts.forEach(product => {
                promises.push(
                  deleteFromCart({
                    productId: product.productId,
                    userId: userInfo.id,
                  }),
                );
              });
              Promise.all(promises).then(() => {
                editUser({
                  ...userInfo,
                  balance: userInfo.balance - sum,
                }).then(response => {
                  dispatch(setUserData(response.data));
                  dispatch(setIsLoading(false));
                  dispatch(setCart([]));
                });
              });
            });
          }
        }}
      >
        Оплатить
      </Btn>
    </div>
  );
}

export default TrashBin;
