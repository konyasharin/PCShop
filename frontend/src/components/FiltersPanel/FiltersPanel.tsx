import React, { ReactNode } from 'react';
import EComponentTypes from 'enums/EComponentTypes.ts';
import styles from './FiltersPanel.module.css';
import MainBtn from 'components/btns/MainBtn/MainBtn.tsx';

type FiltersPanelProps = {
  type: EComponentTypes;
  blocks: ReactNode[];
};

const FiltersPanel: React.FC<FiltersPanelProps> = props => {
  return (
    <div className={styles.panel}>
      {...props.blocks}
      <MainBtn className={styles.btn}>Применить</MainBtn>
    </div>
  );
};

export default FiltersPanel;
