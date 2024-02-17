import React from 'react';
import styles from './Btn.module.css';

type PCBtnProps = {
  children?: string;
};

const Btn: React.FC<PCBtnProps> = props => {
  return <button className={styles.pcBtn}>{props.children}</button>;
};

export default Btn;
