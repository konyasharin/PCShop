import Container from '../Container/Container.tsx';
import logo from '../../assets/logo.png';
import { NavLink } from 'react-router-dom';
import Search from './Search/Search.tsx';
import MainBtn from '../MainBtn/MainBtn.tsx';
import styles from './Header.module.css';
import SearchWindow from './Search/SearchWindow/SearchWindow.tsx';
import useSearch from '../../hooks/useSearch.ts';

function Header() {
  const {
    isActive,
    setIsActiveHandle,
    blocks,
    setBlocks,
    searchWindowRef,
    searchRef,
  } = useSearch();
  console.log(isActive, blocks, setBlocks);
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
        <Search setIsActive={setIsActiveHandle} searchRef={searchRef} />
        <MainBtn className={styles.btn}>авторизация</MainBtn>
        <SearchWindow
          isActive={isActive}
          blocks={blocks}
          searchWindowRef={searchWindowRef}
        />
      </Container>
    </header>
  );
}

export default Header;
