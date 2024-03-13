import AddWindow from '../PCBuildPage/ChooseComponents/ChooseComponent/AddWindow/AddWindow.tsx';
import EComponentTypes from 'enums/EComponentTypes.ts';
import TProduct from 'types/TProduct.ts';
import React from 'react';
import styles from './PCComponents.module.css';
import Container from 'components/Container/Container.tsx';

type PCComponentProps = {
  type: EComponentTypes;
  searchTitle: string;
  products: TProduct[];
};

const PCComponents: React.FC<PCComponentProps> = props => {
  const isActive = true;
  return (
    <Container className={styles.block}>
      <h2 className={styles.text}>Компоненты</h2>
      <AddWindow
        type={props.type}
        searchTitle={props.searchTitle}
        isActive={isActive}
        products={props.products}
      />
    </Container>
  );
};

export default PCComponents;
