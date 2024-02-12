import React from 'react';
import styles from './PCBtn.module.css';

type PCBtnProps = {
  children?: string;
};

const PCBtn: React.FC<PCBtnProps> = props => {
  return <button className={styles.pcBtn}>{props.children}</button>;
};

export default PCBtn;
