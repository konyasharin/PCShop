import styles from './ChooseComponent.module.css';
import React from 'react';
import ErrorCorner from './ErrorCorner/ErrorCorner.tsx';
import createClassNames from 'utils/createClassNames.ts';
import AddWindow from './AddWindow/AddWindow.tsx';
import EComponentTypes from 'enums/EComponentTypes.ts';
import { useDispatch, useSelector } from 'react-redux';
import { setIsActive } from 'store/slices/chooseComponentsSlice.ts';
import { RootState } from 'store/store.ts';
import TProduct from 'types/TProduct.ts';

type ChooseComponentProps = {
  type: EComponentTypes;
  img: string;
  title: string;
  isImportant: boolean;
  errorType: 'Error' | 'Warning' | 'Success';
  className?: string;
  searchTitle: string;
  products: TProduct[];
};

const ChooseComponent: React.FC<ChooseComponentProps> = props => {
  const dispatch = useDispatch();
  const isActive = useSelector(
    (state: RootState) => state.chooseComponents[props.type].isActive,
  );
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
          onClick={() =>
            dispatch(setIsActive({ type: props.type, newIsActive: !isActive }))
          }
        >
          Выбрать
        </div>
        <ErrorCorner type={props.errorType} className={styles.errorCorner} />
      </div>
      <AddWindow
        type={props.type}
        searchTitle={props.searchTitle}
        isActive={isActive}
        products={props.products}
      />
    </div>
  );
};

export default ChooseComponent;
