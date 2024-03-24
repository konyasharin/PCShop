import styles from './ProductCharacteristics.module.css';
import React from 'react';
import TProcessorFilters from 'types/components/TProcessorFilters.ts';
import TVideoCardFilters from 'types/components/TVideoCardFilters.ts';

type ProductCharacteristicsProps = {
  characteristics: TProcessorFilters | TVideoCardFilters;
};

const ProductCharacteristics: React.FC<ProductCharacteristicsProps> = props => {
  return (
    <section className={styles.productCharacteristics}>
      <h2>Характеристики</h2>
      {props.characteristics.brand}
    </section>
  );
};

export default ProductCharacteristics;
