package com.example.pc.Services;

import com.example.pc.Models.ComputerCaseModel;
import com.example.pc.repositories.ComputerCaseRepo;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.stream.Collectors;

@Service
public class ComputerCaseService {

    private final ComputerCaseRepo computerCaseRepo;

    @Autowired
    public ComputerCaseService(ComputerCaseRepo computerCaseRepo) {
        this.computerCaseRepo = computerCaseRepo;
    }

    public List<ComputerCaseModel> getAllCompuerCases() {
        return computerCaseRepo.findAll();
    }

    public ComputerCaseModel getComputerCaseById(Long id) {
        return computerCaseRepo.findById(id).orElse(null);
    }

    public ComputerCaseModel addComputerCase(ComputerCaseModel computerCaseModel) {
        return computerCaseRepo.save(computerCaseModel);
    }

    public void deleteComputerCaseById(Long id) {
        computerCaseRepo.deleteById(id);
    }

}
