import React from 'react';
import styles from './EditAddBtn.module.css';
import plusImg from 'assets/plus.png';
import { Link } from 'react-router-dom';

type EditAddBtnProps = {
  to: string;
};

const EditAddBtn: React.FC<EditAddBtnProps> = props => {
  return (
    <Link to={props.to} className={styles.btn}>
      <img src={plusImg} alt="plus" />
    </Link>
  );
};

export default EditAddBtn;
