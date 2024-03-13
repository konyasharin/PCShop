import React from 'react';
import Card from 'components/cards/Card/Card.tsx';
import MainBtn from 'components/btns/MainBtn/MainBtn.tsx';
import styles from './ChooseFavoritesCard.module.css';
import TrashBin from 'assets/TrashBin.png';

type ChooseFavoriteCard = {
  name: string;
  text: string;
  price: number;
  img: string;
  className?: string;
};

const ChooseFavoritesCard: React.FC<ChooseFavoriteCard> = props => {
  return (
    <Card
      img={props.img}
      className={props.className}
      name={props.name}
      text={props.text}
      bottomBlock={
        <div className={styles.block}>
          <MainBtn className={styles.btn}>Добавить</MainBtn>
          <img src={TrashBin} alt={'TrashBin'} className={styles.bin} />
        </div>
      }
      rightBlock={<h2 className={styles.price}>{props.price}$</h2>}
    />
  );
};

export default ChooseFavoritesCard;
