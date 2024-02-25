import React from 'react';
import styles from './ChooseComponentCard.module.css';
import MainBtn from 'components/MainBtn/MainBtn.tsx';
import createClassNames from 'utils/createClassNames.ts';

type ChooseComponentCard = {
  name: string;
  text: string;
  price: number;
  img: string;
  className?: string;
};

const ChooseComponentCard: React.FC<ChooseComponentCard> = props => {
  return (
    <div className={createClassNames([styles.card, props.className])}>
      <img src={props.img} alt={props.name} />
      <div className={styles.middleBlock}>
        <div className={styles.description}>
          <h5>{props.name}</h5>
          <span>{props.text}</span>
        </div>
        <MainBtn className={styles.btn}>{'Добавить'}</MainBtn>
      </div>
      <h2>{props.price}$</h2>
    </div>
  );
};

export default ChooseComponentCard;
