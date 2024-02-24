import styles from './ChooseComponent.module.css';
import React from 'react';
import ErrorCorner from './ErrorCorner/ErrorCorner.tsx';
import createClassNames from 'utils/createClassNames.ts';
import AddWindow from './AddWindow/AddWindow.tsx';
import TComponentTypes from 'types/TComponentTypes.ts';

type ChooseComponentProps = TComponentTypes & {
  img: string;
  title: string;
  isImportant: boolean;
  errorType: 'Error' | 'Warning' | 'Success';
  className?: string;
};

const ChooseComponent: React.FC<ChooseComponentProps> = props => {
  return (
    <>
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
      <AddWindow type={props.type} />
    </>
  );
};

export default ChooseComponent;
