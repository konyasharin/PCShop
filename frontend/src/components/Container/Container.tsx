import React, { ReactNode } from 'react';
import styles from './Container.module.css';

type ContainerProps = {
  children?: ReactNode;
  className?: string;
};

const Container: React.FC<ContainerProps> = props => {
  return (
    <div className={`${styles.container} ${props.className}`}>
      {props.children}
    </div>
  );
};

export default Container;
