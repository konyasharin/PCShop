import React, { ReactNode } from 'react';
import styles from './FiltersPanel.module.css';
import MainBtn from 'components/btns/MainBtn/MainBtn.tsx';
import createClassNames from 'utils/createClassNames.ts';

type FiltersPanelProps = {
  blocks: ReactNode[];
  className?: string;
};

const FiltersPanel: React.FC<FiltersPanelProps> = props => {
  return (
    <div className={createClassNames([styles.panel, props.className])}>
      {...props.blocks}
      <MainBtn className={styles.btn}>Применить</MainBtn>
    </div>
  );
};

export default FiltersPanel;
