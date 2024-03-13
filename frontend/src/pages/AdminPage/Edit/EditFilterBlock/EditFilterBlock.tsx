import React, { ReactNode } from 'react';
import styles from './EditFilterBlock.module.css';

type EditFilterBlockProps = {
  title: string;
  filterBlock: ReactNode;
  className?: string;
};

const EditFilterBlock: React.FC<EditFilterBlockProps> = props => {
  return (
    <div className={props.className}>
      <h5 className={styles.title}>{props.title}</h5>
      {props.filterBlock}
    </div>
  );
};

export default EditFilterBlock;
