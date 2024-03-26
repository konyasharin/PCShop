import AddWindow from '../PCBuildPage/ChooseComponents/ChooseComponent/AddWindow/AddWindow.tsx';
import componentTypes from 'enums/componentTypes.ts';
import TProduct from 'types/TProduct.ts';
import React from 'react';
import styles from './BuildsPage.module.css';
import Container from 'components/Container/Container.tsx';

type BuildsProps = {
  type: keyof typeof componentTypes;
  searchTitle: string;
  products: TProduct[];
};

const BuildsPage: React.FC<BuildsProps> = props => {
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

export default BuildsPage;
