import styles from './PowerOfBuild.module.css';
import greyQuestion from '../../../assets/greyQuestion.png';
import React, { useEffect, useState } from 'react';

enum color {
  white = '#FFFFFF',
}

const PowerOfBuild: React.FC<{ power: number }> = props => {
  const [circleOpacity, setCircleOpacity] = useState<number[]>([]);
  const price = '';
  useEffect(() => {
    const opacityArray: number[] = [];
    for (let i = 1; i <= 10; i++) {
      if (i <= props.power) {
        opacityArray.push(0.1 * i);
      } else {
        opacityArray.push(i);
      }
    }
    setCircleOpacity(opacityArray);
  }, [props.power]);
  return (
    <div>
      <div className={styles.powerBuild}>
        <h3>Мощность сборки:</h3>
        <h3 className={styles.powerNumber}>{props.power} из 10</h3>
        <div className={styles.greyCircle}>
          <img src={greyQuestion} alt={'greyQuestion'} />
        </div>
        <div className={styles.powerCircles}>
          {Array.from({ length: 10 }).map((_, index) => (
            <div
              key={index}
              className={styles.circle}
              style={{
                backgroundColor:
                  index < props.power
                    ? `rgba(0, 0, 255, ${circleOpacity[index]})`
                    : color.white,
              }}
            ></div>
          ))}
        </div>
      </div>
      <div className={styles.priceBuild}>
        <h3>Итоговая цена:</h3>
        <h3 className={styles.price}>{price}</h3>
      </div>
    </div>
  );
};

export default PowerOfBuild;
