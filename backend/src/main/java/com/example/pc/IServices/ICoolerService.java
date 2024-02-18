package com.example.pc.IServices;

import com.example.pc.Models.CoolerModel;

import java.util.List;

public interface ICoolerService {

    List<CoolerModel> getAllCoolers();
    CoolerModel getCoolerById(Long id);
    CoolerModel addCooler(CoolerModel coolerModel);
    void deleteCoolerById(Long id);
}
