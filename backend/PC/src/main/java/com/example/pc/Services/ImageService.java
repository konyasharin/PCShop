package com.example.pc.Services;

import com.example.pc.Models.ImageModel;
import com.example.pc.repositories.ImageRepo;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class ImageService {

    private final ImageRepo imageRepo;

    @Autowired
    public ImageService(ImageRepo imageRepo) {
        this.imageRepo = imageRepo;
    }

    public List<ImageModel> getAllImages() {
        return imageRepo.findAll();
    }

    public ImageModel getImageById(Long id) {
        return imageRepo.findById(id).orElse(null);
    }

    public ImageModel saveImage(ImageModel imageModel) {
        return imageRepo.save(imageModel);
    }

    public void deleteImageById(Long id) {
        imageRepo.deleteById(id);
    }
}
