import AdminNavigationPanel from 'components/AdminNavigationPanel/AdminNavigationPanel.tsx';
import Container from 'components/Container/Container.tsx';
import styles from './AdminPage.module.css';
import { Route, Routes } from 'react-router-dom';
import Orders from './Orders/Orders.tsx';
import Order from './Order/Order.tsx';
import Roles from './Roles/Roles.tsx';
import Edit from './Edit/Edit.tsx';
import Storage from './Storage/Storage.tsx';

function AdminPage() {
  return (
    <section className={styles.admin}>
      <Container>
        <h2 className={styles.title}>панель администратора</h2>
        <div className={styles.blocks}>
          <AdminNavigationPanel className={styles.panel} />
          <Routes>
            <Route path={'/orders'} element={<Orders />} />
            <Route path={'/orders/:orderNumber'} element={<Order />} />
            <Route path={'/roles'} element={<Roles />} />
            <Route path={'/edit/*'} element={<Edit />} />
            <Route path={'/store'} element={<Storage />} />
          </Routes>
        </div>
      </Container>
    </section>
  );
}

export default AdminPage;
