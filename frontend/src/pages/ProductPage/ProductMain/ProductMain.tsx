import React from 'react';
import styles from './ProductMain.module.css';
import starActiveImg from 'assets/Star-active.png';
import MainBtn from 'components/btns/MainBtn/MainBtn.tsx';

type ProductMainProps = {
  name: string;
  img: string;
  description: string;
  mark: number;
};

const ProductMain: React.FC<ProductMainProps> = props => {
  return (
    <section className={styles.productMain}>
      <img src={props.img} alt={props.name} className={styles.productImg} />
      <div className={styles.rightBlock}>
        <h2>{props.name}</h2>
        <div className={styles.mark}>
          <img src={starActiveImg} alt="star" />
          <h3>{props.mark}</h3>
        </div>
        <span className={styles.description}>{props.description}</span>
        <MainBtn className={styles.btn}>Купить</MainBtn>
      </div>
    </section>
  );
};

export default ProductMain;
