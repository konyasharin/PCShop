import styles from './ChooseComponent.module.css';
import React from 'react';
import ErrorCorner from './ErrorCorner/ErrorCorner.tsx';
import createClassNames from 'utils/createClassNames.ts';

type ChooseComponentProps = {
  img: string;
  title: string;
  isImportant: boolean;
  type:
    | 'videoCard'
    | 'processor'
    | 'cooling'
    | 'RAM'
    | 'motherBoard'
    | 'HDD'
    | 'SSD'
    | 'case'
    | 'powerSupply';
  errorType: 'Error' | 'Warning' | 'Success';
  className?: string;
};

const ChooseComponent: React.FC<ChooseComponentProps> = props => {
  return (
    <div className={createClassNames([styles.block, props.className])}>
      <div className={styles.blockLight}></div>
      <div className={styles.blockDark}></div>
      <img src={props.img} alt={props.type} className={styles.mainImg} />
      <div className={styles.titleTextBlock}>
        <h3 className={props.isImportant ? styles.important : ''}>
          {props.title}
        </h3>
      </div>
      <div className={styles.componentName}>Выбрать</div>
      <ErrorCorner type={props.errorType} className={styles.errorCorner} />
    </div>
  );
};

export default ChooseComponent;
