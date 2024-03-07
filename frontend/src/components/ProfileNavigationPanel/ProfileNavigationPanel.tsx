import styles from './ProfileNavigationPanel.module.css';
import React from 'react';
import createClassNames from 'utils/createClassNames.ts';
import ProfileNavigationPanelLink from 'components/ProfileNavigationPanel/ProfileNavigationPanelLink/ProfileNavigationPanelLink.tsx';

const ProfileNavigationPanel: React.FC<{ className?: string }> = props => {
  return (
    <div className={createClassNames([styles.panel, props.className])}>
      <h3>Навигация</h3>
      <ProfileNavigationPanelLink to={'favorite'} className={styles.link}>
        Избранное
      </ProfileNavigationPanelLink>
      <ProfileNavigationPanelLink to={'trashBin'} className={styles.link}>
        Корзина
      </ProfileNavigationPanelLink>
      <ProfileNavigationPanelLink to={'balance'} className={styles.link}>
        Баланс
      </ProfileNavigationPanelLink>
      <span className={styles.exit}>Выход</span>
    </div>
  );
};

export default ProfileNavigationPanel;
