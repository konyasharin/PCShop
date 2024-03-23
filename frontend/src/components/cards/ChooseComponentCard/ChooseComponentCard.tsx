import React from 'react';
import Card from 'components/cards/Card/Card.tsx';
import MainBtn from 'components/btns/MainBtn/MainBtn.tsx';
import styles from './ChooseComponentCard.module.css';

type ChooseComponentCard = {
  name: string;
  text: string;
  price: number;
  img: string;
  onClick?: () => void;
  className?: string;
};

const ChooseComponentCard: React.FC<ChooseComponentCard> = props => {
  return (
    <Card
      img={props.img}
      className={props.className}
      name={props.name}
      text={props.text}
      bottomBlock={
        <MainBtn className={styles.btn} onClick={props.onClick}>
          Добавить
        </MainBtn>
      }
      rightBlock={<h2 className={styles.price}>{props.price}$</h2>}
    />
  );
};

export default ChooseComponentCard;
