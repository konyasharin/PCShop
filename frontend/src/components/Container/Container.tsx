import React, { ReactNode } from 'react';
import styles from './Container.module.css';
import createClassNames from 'utils/createClassNames.ts';

type ContainerProps = {
  children?: ReactNode;
  className?: string;
};

const Container: React.FC<ContainerProps> = props => {
  return (
    <div className={createClassNames([props.className, styles.container])}>
      {props.children}
    </div>
  );
};

export default Container;
