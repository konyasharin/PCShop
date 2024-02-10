import Container from '../Container/Container.tsx';
import logo from '../../assets/logo.png';
import { NavLink } from 'react-router-dom';
import Search from './Search/Search.tsx';
import MainBtn from '../MainBtn/MainBtn.tsx';
import styles from './Header.module.css';

function Header() {
  return (
    <header className={styles.header}>
      <Container className={styles.container}>
        <img src={logo} alt={'logo'} />
        <NavLink to={'/'} className={styles.link}>
          Главная
        </NavLink>
        <NavLink to={'/'} className={styles.link}>
          Новая сборка
        </NavLink>
        <NavLink to={'/'} className={styles.link}>
          Сборки
        </NavLink>
        <NavLink to={'/'} className={styles.link}>
          Комплектующие
        </NavLink>
        <Search />
        <MainBtn>авторизация</MainBtn>
      </Container>
    </header>
  );
}

export default Header;
