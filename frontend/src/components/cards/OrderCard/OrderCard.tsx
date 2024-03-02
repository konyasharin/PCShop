import React from 'react';
import Card from 'components/cards/Card/Card.tsx';
import styles from './OrderCard.module.css';
import createClassNames from 'utils/createClassNames.ts';

type OrderCardProps = {
  img: string;
  name: string;
  text: string;
  price: number;
  className?: string;
};

const OrderCard: React.FC<OrderCardProps> = props => {
  return (
    <Card
      img={props.img}
      text={props.text}
      name={props.name}
      className={createClassNames([props.className, styles.card])}
      rightBlock={<h2 className={styles.price}>{props.price}$</h2>}
    />
  );
};

export default OrderCard;
