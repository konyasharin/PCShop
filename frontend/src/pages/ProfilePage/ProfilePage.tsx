import ProfileNavigationPanel from 'components/ProfileNavigationPanel/ProfileNavigationPanel.tsx';
import { Route, Routes } from 'react-router-dom';
import Favorites from './Favorites/Favorites.tsx';
import Container from 'components/Container/Container.tsx';
import styles from './ProfilePage.module.css';
import TrashBin from './TrashBin/TrashBin.tsx';
import Balance from './Balance/Balance.tsx';

function ProfilePage() {
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
