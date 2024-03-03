import React, { ReactNode } from 'react';
import createClassNames from 'utils/createClassNames.ts';
import { NavLink } from 'react-router-dom';
import styles from './ToggleBtn.module.css';

type ToggleBtnProps = {
  children?: ReactNode;
  className?: string;
  to: string;
};

const ToggleBtn: React.FC<ToggleBtnProps> = props => {
  return (
    <NavLink
      to={props.to}
      className={({ isActive }) => (isActive ? styles.linkActive : styles.link)}
    >
      <button className={createClassNames([props.className, styles.btn])}>
        {props.children}
      </button>
    </NavLink>
  );
};

export default ToggleBtn;
