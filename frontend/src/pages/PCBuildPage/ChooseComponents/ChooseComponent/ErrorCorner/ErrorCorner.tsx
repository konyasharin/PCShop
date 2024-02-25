import React from 'react';
import styles from './ErrorCorner.module.css';
import createClassNames from 'utils/createClassNames.ts';
import successImg from 'assets/success.png';
import crossImg from 'assets/cross-white.png';
import questionImg from 'assets/question-white.png';

type ErrorCornerProps = {
  className?: string;
  type: 'Error' | 'Warning' | 'Success';
};

const ErrorCorner: React.FC<ErrorCornerProps> = props => {
  let color;
  let img;
  switch (props.type) {
    case 'Warning':
      color = '#F7A400';
      img = questionImg;
      break;
    case 'Error':
      img = crossImg;
      color = '#FF0B0B';
      break;
    case 'Success':
      img = successImg;
      color = '#16D840';
      break;
  }
  return (
    <div
      className={createClassNames([styles.block, props.className])}
      style={{ backgroundColor: color }}
    >
      <div className={styles.circle}>
        <img src={img} alt={props.type} />
      </div>
    </div>
  );
};

export default ErrorCorner;
