import './App.css';
import { Route, Routes } from 'react-router-dom';
import MainPage from './pages/MainPage/MainPage.tsx';
import Header from './components/Header/Header.tsx';
import PCBuildPage from './pages/PCBuildPage/PCBuildPage.tsx';
import Loading from './components/Loading/Loading.tsx';
import Footer from './components/Footer/Footer.tsx';
import { createComputerCase } from 'api/components/createComponents/createComputerCase.ts';
import React from 'react';

function App() {
  return (
    <>
      <input
        type={'file'}
        accept={'image/*, .png, .jpg'}
        onChangeCapture={(e: React.ChangeEvent<HTMLInputElement>) => {
          const formData = new FormData();
          formData.append('file', e.target.files![0]);
          console.log(e.target.files![0]);
          createComputerCase({
            brand: '123',
            model: '12',
            country: 'Russia',
            material: 'plastic',
            width: 32,
            height: 34,
            depth: 50,
            price: 1000,
            description: 'dfsfsdf',
            image: e.target.files![0],
          });
        }}
      />
      <div className={'ellipses'}>
        <div className={'ellipse'} id={'ellipse1'}></div>
        <div className={'ellipse'} id={'ellipse2'}></div>
      </div>
      <Loading />
      <Header />
      <Routes>
        <Route path={'/'} element={<MainPage />} />
        <Route path={'/PCBuild'} element={<PCBuildPage />} />
      </Routes>
      <Footer />
    </>
  );
}

export default App;
