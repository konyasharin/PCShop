package com.example.pc.Services;

import com.example.pc.Models.CoolerModel;
import com.example.pc.Models.MotherBoardModel;
import com.example.pc.repositories.MotherBoardRepo;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class MotherBoardService {

    private final MotherBoardRepo motherBoardRepo;

    @Autowired
    public MotherBoardService(MotherBoardRepo motherBoardRepo) {
        this.motherBoardRepo = motherBoardRepo;
    }

    public List<MotherBoardModel> getMotherBoards() {
        return motherBoardRepo.findAll();
    }

    public MotherBoardModel getMotherBoardById(Long id) {
        return motherBoardRepo.findById(id).orElse(null);
    }

    public MotherBoardModel saveMotherBoard(MotherBoardModel motherBoardModel) {
        return motherBoardRepo.save(motherBoardModel);
    }

    public void deleteCoolerById(Long id) {
        motherBoardRepo.deleteById(id);
    }
}
