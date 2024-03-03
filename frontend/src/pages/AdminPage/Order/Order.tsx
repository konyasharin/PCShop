import { useParams } from 'react-router-dom';
import styles from './Order.module.css';
import createOrderStatusColor from '../createOrderStatusColor.ts';
import EOrderStatuses from 'enums/EOrderStatuses.ts';
import MainBtn from 'components/btns/MainBtn/MainBtn.tsx';
import OrderCard from 'components/cards/OrderCard/OrderCard.tsx';
import videoCardImg from 'assets/videocard.jpg';

function Order() {
  const { orderNumber } = useParams();
  return (
    <div className={styles.order}>
      <div className={styles.orderInfo}>
        <div>Заказ #{orderNumber}</div>
        <div className={styles.status}>
          Статус:{' '}
          <div
            style={{ color: createOrderStatusColor(EOrderStatuses.accepted) }}
            className={styles.statusText}
          >
            Принят
          </div>
        </div>
        <MainBtn className={styles.btn}>В обработке</MainBtn>
      </div>
      <OrderCard
        name={'Gigabyte RTX 3080'}
        text={
          '8GB, здесь типо какие-то характеристики здесь типо какие-то характеристики, здесь типо какие-то харак'
        }
        price={100}
        img={videoCardImg}
      />
    </div>
  );
}

export default Order;
