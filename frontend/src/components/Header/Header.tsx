import Container from '../Container/Container.tsx';
import logo from '../../assets/logo.png';
import { NavLink } from 'react-router-dom';
import Search from './Search/Search.tsx';
import MainBtn from '../MainBtn/MainBtn.tsx';
import styles from './Header.module.css';
import SearchWindow from './Search/SearchWindow/SearchWindow.tsx';
import useWindowSearch from '../../hooks/useWindowSearch.ts';

function Header() {
  const {
    searchWindowIsActive,
    setIsActiveHandle,
    blocks,
    searchWindowRef,
    searchRef,
    searchBtnIsActive,
    setBlocks,
  } = useWindowSearch();
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
          <NavLink to={'/'} className={styles.link}>
            Сборки
          </NavLink>
          <NavLink to={'/'} className={styles.link}>
            Комплектующие
          </NavLink>
        </div>
        <Search
          setIsActive={setIsActiveHandle}
          searchRef={searchRef}
          searchBtnIsActive={searchBtnIsActive}
          setBlocks={setBlocks}
        />
        <MainBtn className={styles.btn}>авторизация</MainBtn>
        <SearchWindow
          isActive={searchWindowIsActive}
          blocks={blocks}
          searchWindowRef={searchWindowRef}
        />
      </Container>
    </header>
  );
}

export default Header;
