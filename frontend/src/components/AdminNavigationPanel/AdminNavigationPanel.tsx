import styles from './AdminNavigationPanel.module.css';
import React from 'react';
import createClassNames from 'utils/createClassNames.ts';
import AdminNavigationPanelLink from 'components/AdminNavigationPanel/AdminNavigationPanelLink/AdminNavigationPanelLink.tsx';

const AdminNavigationPanel: React.FC<{ className?: string }> = props => {
  return (
    <div className={createClassNames([styles.panel, props.className])}>
      <h3>Навигация</h3>
      <AdminNavigationPanelLink to={'orders'} className={styles.link}>
        Заказы
      </AdminNavigationPanelLink>
      <AdminNavigationPanelLink to={'edit'} className={styles.link}>
        Редактирование
      </AdminNavigationPanelLink>
      <AdminNavigationPanelLink to={'roles'} className={styles.link}>
        Выдача ролей
      </AdminNavigationPanelLink>
      <AdminNavigationPanelLink to={'store'} className={styles.link}>
        Склад
      </AdminNavigationPanelLink>
    </div>
  );
};

export default AdminNavigationPanel;
