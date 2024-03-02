import React from 'react';
import EComponentTypes from 'enums/EComponentTypes.ts';
import styles from './FiltersPanel.module.css';
import MainBtn from 'components/btns/MainBtn/MainBtn.tsx';

type FiltersPanelProps = {
  type: EComponentTypes;
  checkBoxesBlocks: React.ReactNode[];
  radiosBlocks: React.ReactNode[];
  inputsBlocks: React.ReactNode[];
};

const FiltersPanel: React.FC<FiltersPanelProps> = props => {
  return (
    <div className={styles.panel}>
      {...props.checkBoxesBlocks}
      {...props.radiosBlocks}
      {...props.inputsBlocks}
      <MainBtn className={styles.btn}>Применить</MainBtn>
    </div>
  );
};

export default FiltersPanel;
