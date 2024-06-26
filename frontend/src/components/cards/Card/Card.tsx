import React, { ReactNode } from 'react';
import styles from './Card.module.css';
import createClassNames from 'utils/createClassNames.ts';
import { Link } from 'react-router-dom';

type CardProps = {
  name: string;
  text: string;
  img: string;
  className?: string;
  bottomBlock?: ReactNode;
  rightBlock?: ReactNode;
  url: string;
};

const Card: React.FC<CardProps> = props => {
  return (
    <div className={createClassNames([styles.card, props.className])}>
      <Link to={props.url}>
        <img src={props.img} alt={props.name} className={styles.mainImg} />
      </Link>
      <div className={styles.middleBlock}>
        <div className={styles.description}>
          <h5>{props.name}</h5>
          <span>{props.text}</span>
        </div>
        {props.bottomBlock}
      </div>
      {props.rightBlock}
    </div>
  );
};

export default Card;
