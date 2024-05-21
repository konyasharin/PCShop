import styles from './ProfileNavigationPanel.module.css';
import React from 'react';
import createClassNames from 'utils/createClassNames.ts';
import ProfileNavigationPanelLink from 'components/ProfileNavigationPanel/ProfileNavigationPanelLink/ProfileNavigationPanelLink.tsx';
import { useNavigate } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { setIsAuth, setUserData } from 'store/slices/profileSlice.ts';

const ProfileNavigationPanel: React.FC<{ className?: string }> = props => {
  const navigate = useNavigate();
  const dispatch = useDispatch();
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
      <span
        className={styles.exit}
        onClick={() => {
          localStorage.removeItem('token');
          dispatch(setUserData(undefined));
          dispatch(setIsAuth(false));
          navigate('/');
        }}
      >
        Выход
      </span>
    </div>
  );
};

export default ProfileNavigationPanel;
