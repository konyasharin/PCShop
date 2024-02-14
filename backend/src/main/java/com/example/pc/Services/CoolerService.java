package com.example.pc.Services;

import com.example.pc.Models.ComputerCaseModel;
import com.example.pc.Models.CoolerModel;
import com.example.pc.repositories.CoolerRepo;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class CoolerService {

    private final CoolerRepo coolerRepo;

    @Autowired
    public CoolerService(CoolerRepo coolerRepo) {
        this.coolerRepo = coolerRepo;
    }

    public List<CoolerModel> getAllCoolers() {
        return coolerRepo.findAll();
    }

    public CoolerModel getCoolerById(Long id) {
        return coolerRepo.findById(id).orElse(null);
    }

    public CoolerModel saveCooler(CoolerModel coolerModel) {
        return coolerRepo.save(coolerModel);
    }

    public void deleteCoolerById(Long id) {
        coolerRepo.deleteById(id);
    }
}
