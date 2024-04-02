import './App.css';
import { Route, Routes } from 'react-router-dom';
import MainPage from './pages/MainPage/MainPage.tsx';
import Header from './components/Header/Header.tsx';
import PCBuildPage from './pages/PCBuildPage/PCBuildPage.tsx';
import Loading from './components/Loading/Loading.tsx';
import Footer from './components/Footer/Footer.tsx';
import LoginPage from './pages/AuthPage/LoginPage/LoginPage.tsx';
import RegistrationPage from './pages/AuthPage/RegistrationPage/RegistrationPage.tsx';
import AdminPage from './pages/AdminPage/AdminPage.tsx';
import ProfilePage from './pages/ProfilePage/ProfilePage.tsx';
import PCComponents from './pages/PCComponents/PCComponents.tsx';
import BuildsPage from './pages/BuildsPage/BuildsPage.tsx';
import ProductPage from './pages/ProductPage/ProductPage.tsx';
import useAllFilters, { TFilters } from 'hooks/useAllFilters.ts';
import { createContext } from 'react';

export const FiltersContext = createContext<TFilters | null>(null);
function App() {
  const { allFilters } = useAllFilters();
  return (
    <>
      <div className={'ellipses'}>
        <div className={'ellipse'} id={'ellipse1'}></div>
        <div className={'ellipse'} id={'ellipse2'}></div>
      </div>
      <FiltersContext.Provider value={allFilters}>
        <Loading />
        <Header />
        <Routes>
          <Route path={'/'} element={<MainPage />} />
          <Route path={'/PCBuild'} element={<PCBuildPage />} />
          <Route path={'/login'} element={<LoginPage />} />
          <Route path={'/registration'} element={<RegistrationPage />} />
          <Route path={'/admin/*'} element={<AdminPage />} />
          <Route path={'/profile/*'} element={<ProfilePage />} />
          <Route
            path={'/components'}
            element={
              <PCComponents
                type={'videoCard'}
                searchTitle={'Выберите видеокарту'}
                products={[]}
              />
            }
          />
          <Route
            path={'/builds'}
            element={
              <BuildsPage
                type={'videoCard'}
                searchTitle={'Выберите видеокарту'}
                products={[]}
              />
            }
          />
          <Route
            path={'/product/:productCategory/:productId'}
            element={<ProductPage />}
          />
        </Routes>
        <Footer />
      </FiltersContext.Provider>
    </>
  );
}

export default App;
