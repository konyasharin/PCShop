import styles from './Scale.module.css';
import lowSmile from 'assets/smile-low.png';
import middleSmile from 'assets/smile-middle.png';
import highSmile from 'assets/smile-high.png';
import React from 'react';

enum colors {
  red = '#FF0B0B',
  orange = '#F7A400',
  green = '#16D840',
}

const Scale: React.FC<{ percents: number }> = props => {
  let colorScaleInner = '';
  let smileImg;
  if (props.percents <= 50) {
    colorScaleInner = colors.red;
    smileImg = lowSmile;
  } else if (props.percents > 50 && props.percents <= 80) {
    colorScaleInner = colors.orange;
    smileImg = middleSmile;
  } else if (props.percents > 80) {
    colorScaleInner = colors.green;
    smileImg = highSmile;
  }
  return (
    <div className={styles.scale}>
      <img src={smileImg} alt="smile" />
      <div
        className={styles.scaleInner}
        style={{
          width: `calc(${props.percents}% + 6px)`,
          backgroundColor: `${colorScaleInner}`,
        }}
      ></div>
    </div>
  );
};

export default Scale;
