import React, { ReactNode } from 'react';
import { NavLink } from 'react-router-dom';
import createClassNames from 'utils/createClassNames.ts';
import styles from './AdminNavigationPanelLink.module.css';

type AdminNavigationPanelLinkProps = {
  children: ReactNode;
  to: string;
  className?: string;
};

const AdminNavigationPanelLink: React.FC<
  AdminNavigationPanelLinkProps
> = props => {
  return (
    <NavLink
      className={createClassNames([props.className, styles.link])}
      to={props.to}
      style={({ isActive }) => ({
        textDecoration: isActive ? 'underline' : 'none',
      })}
    >
      {props.children}
    </NavLink>
  );
};

export default AdminNavigationPanelLink;
