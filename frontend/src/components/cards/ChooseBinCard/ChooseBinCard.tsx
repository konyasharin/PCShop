import React from 'react';
import Card from 'components/cards/Card/Card.tsx';
import styles from './ChooseBinCard.module.css';
import TrashBin from 'assets/TrashBin.png';

type ChooseBinCard = {
  name: string;
  text: string;
  price: number;
  img: string;
  onDelete: () => void;
  url: string;
  className?: string;
};

const ChooseBinCard: React.FC<ChooseBinCard> = props => {
  return (
    <Card
      img={props.img}
      className={props.className}
      name={props.name}
      text={props.text}
      bottomBlock={<h2 className={styles.price}>{props.price}$</h2>}
      url={props.url}
      rightBlock={
        <img
          onClick={props.onDelete}
          src={TrashBin}
          alt={'TrashBin'}
          className={styles.bin}
        />
      }
    />
  );
};

export default ChooseBinCard;
