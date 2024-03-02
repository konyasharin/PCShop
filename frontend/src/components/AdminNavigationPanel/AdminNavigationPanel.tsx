import styles from './AdminNavigationPanel.module.css';
import React from 'react';
import createClassNames from 'utils/createClassNames.ts';
import AdminNavigationPanelLink from 'components/AdminNavigationPanel/AdminNavigationPanelLink/AdminNavigationPanelLink.tsx';

const AdminNavigationPanel: React.FC<{ className?: string }> = props => {
  return (
    <div className={createClassNames([styles.panel, props.className])}>
      <h3>Навигация</h3>
      <AdminNavigationPanelLink to={'/admin/orders'} className={styles.link}>
        Заказы
      </AdminNavigationPanelLink>
      <AdminNavigationPanelLink to={'/admin/edit'} className={styles.link}>
        Редактирование
      </AdminNavigationPanelLink>
      <AdminNavigationPanelLink to={'/admin/roles'} className={styles.link}>
        Выдача ролей
      </AdminNavigationPanelLink>
      <AdminNavigationPanelLink to={'/admin/store'} className={styles.link}>
        Склад
      </AdminNavigationPanelLink>
    </div>
  );
};

export default AdminNavigationPanel;
