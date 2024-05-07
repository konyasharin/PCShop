import React from 'react';
import orderStatuses from 'enums/orderStatuses.ts';
import styles from './OrderBlock.module.css';
import createClassNames from 'utils/createClassNames.ts';
import { Link } from 'react-router-dom';
import createOrderStatusColor from '../../createOrderStatusColor.ts';

type OrderBlockProps = {
  orderNumber: number;
  status: keyof typeof orderStatuses;
  className?: string;
};

const OrderBlock: React.FC<OrderBlockProps> = props => {
  return (
    <Link to={`/admin/orders/${props.orderNumber}`} className={styles.link}>
      <div className={createClassNames([styles.block, props.className])}>
        <h5>Заказ #{props.orderNumber}</h5>
        <h5 style={{ color: createOrderStatusColor(props.status) }}>
          {orderStatuses[props.status]}
        </h5>
      </div>
    </Link>
  );
};

export default OrderBlock;
