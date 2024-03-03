import './App.css';
import { Route, Routes } from 'react-router-dom';
import MainPage from './pages/MainPage/MainPage.tsx';
import Header from './components/Header/Header.tsx';
import PCBuildPage from './pages/PCBuildPage/PCBuildPage.tsx';
import Loading from './components/Loading/Loading.tsx';
import Footer from './components/Footer/Footer.tsx';
import { createComputerCase } from 'api/components/createComponents/createComputerCase.ts';
import React from 'react';
import LoginPage from './pages/AughPage/LoginPage/LoginPage.tsx';
import RegistrationPage from './pages/AughPage/RegistrationPage/RegistrationPage.tsx';
import AdminPage from './pages/AdminPage/AdminPage.tsx';

function App() {
  return (
    <>
      <div className={'ellipses'}>
        <div className={'ellipse'} id={'ellipse1'}></div>
        <div className={'ellipse'} id={'ellipse2'}></div>
      </div>
      <Loading />
      <Header />
      <Routes>
        <Route path={'/'} element={<MainPage />} />
        <Route path={'/PCBuild'} element={<PCBuildPage />} />
        <Route path={'/login'} element={<LoginPage />} />
        <Route path={'/registration'} element={<RegistrationPage />} />
        <Route path={'/admin/*'} element={<AdminPage />} />
      </Routes>
      <Footer />
    </>
  );
}

export default App;
