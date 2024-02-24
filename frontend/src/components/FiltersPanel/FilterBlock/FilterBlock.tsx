import React from 'react';
import styles from './FilterBlock.module.css';

type FilterBlock = {
  children?: React.ReactNode;
  title: string;
};

const FilterBlock: React.FC<FilterBlock> = props => {
  return (
    <div>
      <h6 className={styles.title}>{props.title}</h6>
      <div className={styles.block}>{props.children}</div>
    </div>
  );
};

export default FilterBlock;
