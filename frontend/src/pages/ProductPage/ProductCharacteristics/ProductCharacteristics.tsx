import styles from './ProductCharacteristics.module.css';
import React from 'react';
import { TProductCharacteristic } from '../ProductPage.tsx';

type ProductCharacteristicsProps = {
  characteristics: TProductCharacteristic[];
};

const ProductCharacteristics: React.FC<ProductCharacteristicsProps> = props => {
  const characteristicsBlocks = props.characteristics.map(characteristic => {
    return (
      <div className={styles.characteristicBlock}>
        <div className={styles.characteristicName}>
          {characteristic.characteristicName}
        </div>
        <div>{characteristic.value}</div>
      </div>
    );
  });
  return (
    <section className={styles.productCharacteristics}>
      <h2>Характеристики</h2>
      <div className={styles.characteristicsBlock}>
        {...characteristicsBlocks}
      </div>
    </section>
  );
};

export default ProductCharacteristics;
