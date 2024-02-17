import Container from '../../components/Container/Container.tsx';
import styles from './PCBuildPage.module.css';
import { useState } from 'react';

function PCBuildPage() {
  const [scaleInnerPercents] = useState(30);
  let colorScaleInner = '';
  if (scaleInnerPercents <= 50) {
    colorScaleInner = '#FF0B0B';
  } else if (scaleInnerPercents > 50 && scaleInnerPercents <= 80) {
    colorScaleInner = '#F7A400';
  } else if (scaleInnerPercents > 80) {
    colorScaleInner = '#16D840';
  }
  return (
    <Container>
      <div className={styles.scale}>
        <div
          className={styles.scaleInner}
          style={{
            width: `calc(${scaleInnerPercents}% + 6px)`,
            backgroundColor: `${colorScaleInner}`,
          }}
        ></div>
      </div>
    </Container>
  );
}

export default PCBuildPage;
