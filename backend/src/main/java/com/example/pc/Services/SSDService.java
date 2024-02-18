package com.example.pc.Services;

import com.example.pc.IServices.ISSDService;
import com.example.pc.Models.SSDModel;
import com.example.pc.repositories.SSDRepo;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class SSDService implements ISSDService {

    private final SSDRepo ssdRepo;

    @Autowired
    public SSDService(SSDRepo ssdRepo) {
        this.ssdRepo = ssdRepo;
    }

    @Override
    public List<SSDModel> getSSD() {
        return ssdRepo.findAll();
    }

    @Override
    public SSDModel getSSDById(Long id) {
        return ssdRepo.findById(id).orElse(null);
    }

    @Override
    public SSDModel addSSD(SSDModel ssdModel) {
        return ssdRepo.save(ssdModel);
    }

    @Override
    public void deleteSSDById(Long id) {
        ssdRepo.deleteById(id);
    }

}
