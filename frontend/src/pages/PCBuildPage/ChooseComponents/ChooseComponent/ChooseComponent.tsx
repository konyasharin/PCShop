import styles from './ChooseComponent.module.css';
import React from 'react';
import ErrorCorner from './ErrorCorner/ErrorCorner.tsx';
import createClassNames from 'utils/createClassNames.ts';
import AddWindow from './AddWindow/AddWindow.tsx';
import componentTypes from 'enums/componentTypes.ts';
import TProduct from 'types/TProduct.ts';

type ChooseComponentProps = {
  type: keyof typeof componentTypes;
  img: string;
  title: string;
  isImportant: boolean;
  errorType: 'Error' | 'Warning' | 'Success';
  className?: string;
  searchTitle: string;
  products: TProduct[] | null;
  onShowMore: () => void;
  currentProduct: TProduct | null;
  setCurrentProduct: (
    newProduct: TProduct | null,
    componentType: keyof typeof componentTypes,
  ) => void;
  isActive: boolean;
  setIsActive: (
    componentType: keyof typeof componentTypes,
    newIsActive: boolean,
  ) => void;
};

const ChooseComponent: React.FC<ChooseComponentProps> = props => {
  return (
    <div className={styles.fullBlock}>
      <div className={createClassNames([styles.block, props.className])}>
        <div className={styles.blockLight}></div>
        <div className={styles.blockDark}></div>
        <img src={props.img} alt={props.type} className={styles.mainImg} />
        <div className={styles.titleTextBlock}>
          <h3 className={props.isImportant ? styles.important : ''}>
            {props.title}
          </h3>
        </div>
        <div
          className={styles.componentName}
          onClick={() => props.setIsActive(props.type, !props.isActive)}
        >
          {props.currentProduct ? props.currentProduct.name : 'Выбрать'}
        </div>
        <ErrorCorner type={props.errorType} className={styles.errorCorner} />
      </div>
      <AddWindow
        type={props.type}
        searchTitle={props.searchTitle}
        isActive={props.isActive}
        products={props.products}
        onShowMore={props.onShowMore}
        setCurrentProduct={props.setCurrentProduct}
      />
    </div>
  );
};

export default ChooseComponent;
