import React, { ReactNode } from 'react';
import styles from './Container.module.css';

type ContainerProps = {
  children?: ReactNode;
};

const Container: React.FC<ContainerProps> = props => {
  return <div className={styles.container}>{props.children}</div>;
};

export default Container;
