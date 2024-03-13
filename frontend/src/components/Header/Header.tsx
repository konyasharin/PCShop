import Container from '../Container/Container.tsx';
import logo from '../../assets/logo.png';
import { NavLink } from 'react-router-dom';
import Search from './Search/Search.tsx';
import MainBtn from 'components/btns/MainBtn/MainBtn.tsx';
import styles from './Header.module.css';
import SearchWindow from './Search/SearchWindow/SearchWindow.tsx';
import { useSelector } from 'react-redux';
import { RootState } from 'store/store.ts';

function Header() {
  const isAuth = useSelector((state: RootState) => state.profile.isAuth);
  return (
    <header className={styles.header}>
      <Container className={styles.container}>
        <img src={logo} alt={'logo'} className={styles.logo} />
        <div className={styles.links}>
          <NavLink to={'/'} className={styles.link}>
            Главная
          </NavLink>
          <NavLink to={'/PCBuild'} className={styles.link}>
            Новая сборка
          </NavLink>
          <NavLink to={'/builds'} className={styles.link}>
            Сборки
          </NavLink>
          <NavLink to={'/components'} className={styles.link}>
            Комплектующие
          </NavLink>
        </div>
        <Search />
        <NavLink to={isAuth ? '/profile' : '/login'} className={styles.linkBtn}>
          <MainBtn className={styles.btn}>
            {isAuth ? 'профиль' : 'авторизация'}
          </MainBtn>
        </NavLink>
        <SearchWindow />
      </Container>
    </header>
  );
}

export default Header;
