import styles from './PowerOfBuild.module.css';
import greyQuestion from '../../../assets/greyQuestion.png';
import React from 'react';

enum color {
  white = '#FFFFFF',
}

const PowerOfBuild: React.FC<{ power: number; price: string }> = props => {
  const opacityArray: number[] = [];
  for (let i = 1; i <= 10; i++) {
    if (i <= props.power) {
      opacityArray.push(0.1 * i);
    } else {
      opacityArray.push(0);
    }
  }
  const circlesArray = opacityArray.map((circle, i) => {
    return (
      <div
        key={i}
        className={styles.circle}
        style={{
          backgroundColor:
            i < props.power ? `rgba(0, 0, 255, ${circle})` : color.white,
        }}
      ></div>
    );
  });
  return (
    <div>
      <div className={styles.powerBuild}>
        <h3>Мощность сборки:</h3>
        <h3 className={styles.powerNumber}>{props.power} из 10</h3>
        <div className={styles.greyCircle}>
          <img src={greyQuestion} alt={'greyQuestion'} />
        </div>
        <div className={styles.powerCircles}>{...circlesArray}</div>
      </div>
      <div className={styles.priceBuild}>
        <h3>Итоговая цена:</h3>
        <h3 className={styles.price}>{props.price}</h3>
      </div>
    </div>
  );
};

export default PowerOfBuild;
