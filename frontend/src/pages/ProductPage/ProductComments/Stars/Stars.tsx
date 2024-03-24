import React, { SetStateAction } from 'react';
import activeStar from 'assets/Star-active.png';
import disableStar from 'assets/Star-disable.png';
import styles from './Stars.module.css';

type StarsProps = {
  mark: number;
  setMark?: React.Dispatch<SetStateAction<number>>;
  className?: string;
};

const Stars: React.FC<StarsProps> = props => {
  const stars = [];
  for (let i = 0; i < 5; i++) {
    let star: string;
    if (props.mark < i + 1) {
      star = disableStar;
    } else {
      star = activeStar;
    }
    stars.push(
      <img
        src={star}
        alt="star"
        className={styles.star}
        style={{ cursor: props.setMark ? 'pointer' : 'auto' }}
        onClick={() => {
          if (props.setMark) {
            props.setMark(i + 1);
          }
        }}
      />,
    );
  }
  return <div className={props.className}>{...stars}</div>;
};

export default Stars;
