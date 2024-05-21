import ProfileNavigationPanel from 'components/ProfileNavigationPanel/ProfileNavigationPanel.tsx';
import { Route, Routes, useNavigate } from 'react-router-dom';
import Favorites from './Favorites/Favorites.tsx';
import Container from 'components/Container/Container.tsx';
import styles from './ProfilePage.module.css';
import TrashBin from './TrashBin/TrashBin.tsx';
import Balance from './Balance/Balance.tsx';
import { useSelector } from 'react-redux';
import { RootState } from 'store/store.ts';
import { useEffect } from 'react';

function ProfilePage() {
  const navigate = useNavigate();
  const userInfo = useSelector((state: RootState) => state.profile.userInfo);
  useEffect(() => {
    if (!userInfo) {
      navigate('/');
    }
  }, [userInfo]);

  return (
    <>
      <h2 className={styles.profile}>Профиль</h2>
      <Container className={styles.block}>
        <ProfileNavigationPanel className={styles.navigation} />
        <div>
          <Routes>
            <Route path={'/favorite'} element={<Favorites />} />
            <Route path={'/trashBin'} element={<TrashBin />} />
            <Route path={'/Balance'} element={<Balance />} />
          </Routes>
        </div>
      </Container>
    </>
  );
}

export default ProfilePage;
